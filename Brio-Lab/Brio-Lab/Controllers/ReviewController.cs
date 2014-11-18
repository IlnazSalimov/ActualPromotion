
using Brio;
using Brio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BrioLab
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
            return View(reviewRepository.GetCompanyReviews(AppSettings.CurrentCompany));
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
        public ActionResult Add(ReviewContent postReview, HttpPostedFileBase LinkToImg)
        {
            if (ModelState.IsValid && (LinkToImg != null && LinkToImg.ContentLength > 0))
            {
                Review review = new Review();

                review.AuthorCompany = postReview.AuthorCompany;
                review.AutorPosition = postReview.AuthorPosition;
                review.CompanyId = AppSettings.CurrentCompany;
                review.Date = DateTime.Now;
                review.ReviewMessage = postReview.Message;
                review.Title = postReview.AuthorName;

                /*Сохранение фото*/
                var fileName = Path.GetFileName(LinkToImg.FileName);
                var savingPath = Path.Combine(HttpContext.Server.MapPath(photoUploadDirecory), fileName);
                LinkToImg.SaveAs(savingPath);
                review.AuthorPhoto = VirtualPathUtility.ToAbsolute(Path.Combine(photoUploadDirecory, fileName));

                reviewRepository.Insert(review);
                reviewRepository.SaveChanges();

                return RedirectToAction("GetAll");
                //throw new HttpException(403, "Forbidden");
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
