using System;
using System.Collections.Generic;
using AjaxControlToolkit;
using Fisharoo.FisharooCore.Core;
using Fisharoo.FisharooCore.Core.DataAccess;
using Fisharoo.FisharooWeb.UserControls.Interfaces;
using StructureMap;

namespace Fisharoo.FisharooWeb.UserControls.Presenters
{
    public class RatingsPresenter
    {
        private IRatings _view;
        private IWebContext _webContext;
        private ISystemObjectRatingOptionRepository _systemObjectRatingOptionRepository;
        private IRatingRepository _ratingRepository;
        public RatingsPresenter()
        {
            _webContext = ObjectFactory.GetInstance<IWebContext>();
            _systemObjectRatingOptionRepository = ObjectFactory.GetInstance<ISystemObjectRatingOptionRepository>();
            _ratingRepository = ObjectFactory.GetInstance<IRatingRepository>();
        }

        public void Init(IRatings view, bool IsPostBack)
        {
            _view = view;
            LoadOptions(_view.SystemObjectID, _view.SystemObjectRecordID);

            //not logged in? Can't add ratings
            if(_webContext.CurrentUser == null)
                _view.CanSetRating(false);
            //already rated this? Can't add ratings
            else if (_ratingRepository.HasRatedBefore(_view.SystemObjectID, _view.SystemObjectRecordID, _webContext.CurrentUser.AccountID))
                _view.CanSetRating(false);
            //ok ok...go ahead and rate this
            else
                _view.CanSetRating(true);

            _view.SetCurrentRating(_ratingRepository.GetCurrentRating(_view.SystemObjectID, _view.SystemObjectRecordID));
        }

        public void LoadOptions(int SystemObjectID, long SystemObjectRecordID)
        {
            _view.LoadOptions(_systemObjectRatingOptionRepository.GetSystemObjectRatingOptionsBySystemObjectID(SystemObjectID));
        }

        public void rating_Changed(object sender, RatingEventArgs args)
        {
            AjaxControlToolkit.Rating rating = sender as AjaxControlToolkit.Rating;

            //add slected ratings to the session handler and make it a dictionary object instead or a custom structure
            Dictionary<int, int> newRating = new Dictionary<int, int>();
            newRating.Add(Convert.ToInt32(rating.Tag), Convert.ToInt32(args.Value));
            _webContext.SelectedRatings = newRating;
        }

        public void btnSave_Click(object sender, EventArgs e, int SystemObjectID, long SystemObjectRecordID)
        {
            Dictionary<int, int> selectedRatings = _webContext.SelectedRatings;
            List<FisharooCore.Core.Domain.Rating> ratings = new List<FisharooCore.Core.Domain.Rating>();
            if(selectedRatings != null)
            {
                foreach (KeyValuePair<int, int> pair in selectedRatings)
                {
                    FisharooCore.Core.Domain.Rating rating = new FisharooCore.Core.Domain.Rating();
                    rating.CreatedByAccountID = _webContext.CurrentUser.AccountID;
                    rating.CreatedByUsername = _webContext.CurrentUser.Username;
                    rating.CreateDate = DateTime.Now;
                    rating.Score = pair.Value;
                    rating.SystemObjectRatingOptionID = pair.Key;
                    rating.SystemObjectID = SystemObjectID;
                    rating.SystemObjectRecordID = SystemObjectRecordID;
                    ratings.Add(rating);
                }
                _ratingRepository.SaveRatings(ratings);
            }
            _webContext.ClearSelectedRatings();
        }
    }
}
