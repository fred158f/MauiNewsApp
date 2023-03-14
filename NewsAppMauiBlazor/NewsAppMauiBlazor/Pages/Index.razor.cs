using Microsoft.AspNetCore.Components;
using NewsAppMauiBlazor.Domain.Models;
using NewsAppMauiBlazor.Infrastucture.DtoModels;
using NewsAppMauiBlazor.Toolbox.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAppMauiBlazor.Pages
{
    public partial class Index
    {
        private NewsModel[] News { get; set; }
        private string[] feeds;
        private string activeFeed;

        protected override void OnInitialized()
        {
            feeds = newsService.GetFeedNames();
            activeFeed = feeds[0];
            UpdateNews();
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            UpdateNews();
            base.OnParametersSet();
        }

        private void OnRadioSelect(ChangeEventArgs e)
        {
            activeFeed = e.Value.ToString();
            UpdateNews();
        }

        private async void UpdateNews()
        {
            bool? filter = simpleStorage.Get<bool?>("filterActivated", null);
            if (filter == true)
            {
                FilterRule[] filterRules;
                FilterRuleDtoModel[] filterRuleDtos = await storageService.GetCollection<FilterRuleDtoModel>(nameof(filterRules));
                filterRules = new FilterRule[filterRuleDtos.Length];

                for (int i = 0; i < filterRuleDtos.Length; i++)
                {
                    FilterRuleDtoModel model = filterRuleDtos[i];
                    filterRules[i] = new FilterRule(model.Value, model.Name, (ERuleType)model.FilterType);
                }
                NewsFilter newsFilter = new NewsFilter(filterRules);
                News = newsService.GetAll(activeFeed, newsFilter);
            }
            else
            {
                News = newsService.GetAll(activeFeed);
            }
            StateHasChanged();
        }
    }
}
