using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using simpleBlog.Contracts.Repositories;
using simpleBlog.Model;
using System.Data.Entity;
using simpleBlog.WebUI.Infrastructure;
using simpleBlog.WebUI.Areas.Admin.ViewModel;
using simpleBlog.WebUI.Infrastructure.Extensions;

namespace simpleBlog.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    
    public class PostsController : Controller
    {

        private IBaseRepository<Post> _posts;
        private IBaseRepository<User> _users;
        private IBaseRepository<Tag> _tags;

        public PostsController(IBaseRepository<Post> posts, IBaseRepository<User> users, IBaseRepository<Tag> tags)
        {
            this._posts = posts;
            this._users = users;
            this._tags = tags;
        }


        public const int postPerPage = 5;
        public ActionResult Index(int page = 1)
        {
            var postCount = _posts.GetAll().Count();

            var currentPostPage = _posts.GetAll()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * postPerPage)
                .Take(postPerPage)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostPage, postCount, page, postPerPage)
            });
        }

        public ActionResult Create()
        {
            return View("postForm", new PostViewModel
            {
                isNew = true,
                Tags = _tags.GetAll().Select(t => new TagCheckbox { 
                   TagId = t.TagId,
                   Name = t.Name,
                   isChecked = false
                }).ToList()
            });
        }


        public ActionResult Edit(int id)
        {
            var post = _posts.GetById(id);
            if (post == null)
                return HttpNotFound();

            return View("postForm", new PostViewModel
            {
                isNew = false,
                PostId =id,
                Title = post.Title,
                Slug = post.Slug,
                Content = post.Content,
                Tags = _tags.GetAll().Select(t => new TagCheckbox {
                    TagId = t.TagId,
                    Name = t.Name,
                    isChecked = false
                }).ToList(),
            });
        }

        [HttpPost]
        public ActionResult postForm(PostViewModel model)
        {

            if (!ModelState.IsValid)
                return View(new PostViewModel {
                   isNew = model.isNew,
                    Tags = _tags.GetAll().Select(t => new TagCheckbox
                    {
                        TagId = t.TagId,
                        Name = t.Name,
                        isChecked = false
                    }).ToList()
                });

            model.isNew = model.PostId == null;

            var user = _users.GetAll().FirstOrDefault(u => u.Name == HttpContext.User.Identity.Name);
            var selectedTags = ReconsileTags(model.Tags).ToList();

            Post post;
            if (model.isNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.Now,
                    UserId = user.UserId,

                };

                foreach (var tag in selectedTags)
                {
                    post.Tags.Add(tag);
                }
            }
            else
            {
                post = _posts.GetById(model.PostId);

                if (post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.Now;

                foreach (var toAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                        post.Tags.Add(toAdd);

                foreach (var toRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToList())
                    post.Tags.Remove(toRemove);

            }

            post.Title = model.Title;
            post.Slug = model.Slug;
            post.Content = model.Content;
            Console.WriteLine(post);
            _posts.Insert(post);
            _posts.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Trash(int id)
        {
            var post = _posts.GetById(id);
            if (post == null)
                return HttpNotFound();

            post.DeletetAt = DateTime.Now;
            _posts.Update(post);
            _posts.Commit();
            return RedirectToAction("Index");
        }
        
        public ActionResult Restore( int id)
        {
            var post = _posts.GetById(id);
            if (post == null)
                return HttpNotFound();

            post.DeletetAt = null;
            _posts.Update(post);
            _posts.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _posts.Delete(id);
            _posts.Commit();

            return RedirectToAction("Index");
        }

        
        public IEnumerable<Tag> ReconsileTags(IEnumerable<TagCheckbox> tags)
        {
            foreach(var tag in tags.Where(t => t.isChecked))
            {
                if(tag.TagId != null)
                {
                    yield return _tags.GetById(tag.TagId);
                    continue;
                }

                var existingTag = _tags.GetAll().FirstOrDefault(t => t.Name == tag.Name);

                if(existingTag !=null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag
                {
                    Name = tag.Name,
                    Slug = tag.Name.Slugify()
                };

                _tags.Insert(newTag);
                _tags.Commit();

                yield return newTag;

            }
        }





    }
}