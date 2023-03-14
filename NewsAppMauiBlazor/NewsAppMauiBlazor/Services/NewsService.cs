using NewsAppMauiBlazor.Domain.Models;
using NewsAppMauiBlazor.Toolbox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NewsAppMauiBlazor.Services
{
    public class NewsService
    {
        XmlReader reader;

        public NewsService()
        {
        }

        public enum ENewsFeed
        {
            SenesteNyt,
            Indland,
            Udland,
            Penge,
            Politik,
            Sporten,
            SenesteSport,
            Viden,
            Kultur,
            Musik,
            MitLiv,
            Mad,
            Vejret,
            NbrOfItems
        }

        public NewsModel[] GetAll(string _feed, IFilter<NewsModel> filter = null)
        {
            if (_feed == Enum.GetName(ENewsFeed.NbrOfItems))
                throw new ArgumentException("Type note supported");

            List<NewsModel> news = new List<NewsModel>();
            reader = XmlReader.Create($"https://www.dr.dk/nyheder/service/feeds/{_feed.ToLower()}");
            SyndicationFeed feed = SyndicationFeed.Load(reader);
           
            foreach(SyndicationItem item in feed.Items)
            {
                news.Add(new NewsModel()
                {
                    Title = item.Title.Text,
                    SourceLink = item.Links[0]?.Uri.AbsoluteUri,
                    Summary = item.Summary?.Text,
                    TimeStamp = item.PublishDate.LocalDateTime
                });
            }

            if (filter != null)
            {
                return news.FindAll(x => filter.Catch(x)).ToArray();
            }
            NewsModel[] returnArray = news.ToArray();
            Array.Sort(returnArray, (x, y) => { return (x.TimeStamp < y.TimeStamp) ? -1 : 1; });
            return returnArray;
        }



        public string[] GetFeedNames()
        {
            List<string> names = new List<string>();
            for (int i = 0; i < (int)ENewsFeed.NbrOfItems; i++)
            {
                names.Add(Enum.GetName((ENewsFeed)i));
            }
            return names.ToArray();
        }


    }
}
