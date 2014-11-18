
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TIK
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository reviewRepository;
        private string photoUploadDirecory = "//Files//Documents//";

        public ReviewController(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public ActionResult GetAll()
        {
            return View(reviewRepository.GetCompanyReviews(AppSettings.CurrentCompany.ID));
        }


        public JsonResult GetReviewContent(int reviewId)
        {
            Review review = reviewRepository.GetById(reviewId);
            ReviewContent response = new ReviewContent
            {
                AuthorName = review.Title,
                AuthorPosition = review.AutorPosition,
                Message = review.ReviewMessage,
                AuthorCompany = review.AuthorCompany,
                AuthorAvatar = review.AuthorPhoto
            };
            return Json(response);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult Add(TIK.ReviewContent postReview, HttpPostedFileBase LinkToImg)
        {
            if (ModelState.IsValid)
            {
                Review review = new Review();

                review.AuthorCompany = postReview.AuthorCompany;
                review.AutorPosition = postReview.AuthorPosition;
                review.CompanyId = AppSettings.CurrentCompany.ID;
                review.Date = DateTime.Now;
                review.ReviewMessage = postReview.Message;
                review.Title = postReview.AuthorName;

                if (LinkToImg != null && LinkToImg.ContentLength > 0)
                {
                    /*Сохранение фото*/
                    var fileName = Path.GetFileName(LinkToImg.FileName);
                    var savingPath = Path.Combine(HttpContext.Server.MapPath(photoUploadDirecory), fileName);
                    LinkToImg.SaveAs(savingPath);
                    review.AuthorPhoto = VirtualPathUtility.ToAbsolute(Path.Combine(photoUploadDirecory, fileName));
                }

                reviewRepository.Insert(review);
                reviewRepository.SaveChanges();

                return RedirectToAction("GetAll");
                //throw new HttpException(403, "Forbidden");
            }
            else
                return View(postReview);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Review review = reviewRepository.GetById(id);
            return View(new EditReviewContent
            {
                ID = review.ID,
                AuthorAvatar = review.AuthorPhoto,
                AuthorCompany = review.AuthorCompany,
                AuthorName = review.Title,
                AuthorPosition = review.AutorPosition,
                Message = review.ReviewMessage
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(EditReviewContent postReview)
        {
            if (ModelState.IsValid)
            {
                Review review = reviewRepository.GetById(postReview.ID);

                review.AuthorCompany = postReview.AuthorCompany;
                review.AutorPosition = postReview.AuthorPosition;
                review.CompanyId = AppSettings.CurrentCompany.ID;
                review.Date = DateTime.Now;
                review.ReviewMessage = postReview.Message;
                review.Title = postReview.AuthorName;

                reviewRepository.Update(review);
                reviewRepository.SaveChanges();

                return RedirectToAction("GetAll");
            }
            else
                return View(postReview);
        }

        public JsonResult Delete(int id)
        {
            Review review = reviewRepository.GetById(id);
            reviewRepository.Delete(review);
            return Json(new {success = true});
        }
    }
}
