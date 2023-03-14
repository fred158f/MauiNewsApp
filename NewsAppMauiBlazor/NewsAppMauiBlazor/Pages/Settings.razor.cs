using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using NewsAppMauiBlazor.Components;
using NewsAppMauiBlazor.Infrastucture.DtoModels;
using NewsAppMauiBlazor.Toolbox.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAppMauiBlazor.Pages
{
    public partial class Settings
    {
        [CascadingParameter]
        public IModalService Modal { get; set; }

        private FilterRule[] filterRules;
        private bool? filterActivated { get; set; } = false;

        private void SetParameters()
        {
            //ModalParameters parameters = new ModalParameters();
            //parameters.Add("filterRules", nameof(filterRules));
            Modal.Show<AddFilter>("Add filter");
        }

        private void ClearFilters()
        {
            storageService.ClearStorage();
            UpdateView();
        }

        private void EnableFilter()
        {
            filterActivated = !filterActivated;
            simpleStorage.Set(nameof(filterActivated), filterActivated);
            UpdateView();
        }

        protected override async Task OnInitializedAsync()
        {
            bool? filterSaved = simpleStorage.Get<bool?>(nameof(filterActivated), null);

            if (filterSaved == null)
                simpleStorage.Set(nameof(filterActivated), filterActivated);

            await base.OnInitializedAsync();
        }

        public async void UpdateView()
        {
            filterActivated = simpleStorage.Get<bool?>(nameof(filterActivated), null);
            filterRules = null;
            if (filterActivated == true)
            {
                FilterRuleDtoModel[] filterRuleDtos = await storageService.GetCollection<FilterRuleDtoModel>(nameof(filterRules));
                filterRules = new FilterRule[filterRuleDtos.Length];

                for (int i = 0; i < filterRuleDtos.Length; i++)
                {
                    FilterRuleDtoModel model = filterRuleDtos[i];
                    filterRules[i] = new FilterRule(model.Value, model.Name, (ERuleType)model.FilterType);
                }
            }
            StateHasChanged();
        }

        protected override async Task OnParametersSetAsync()
        {
            UpdateView();

            await base.OnParametersSetAsync();
        }
    }
}
