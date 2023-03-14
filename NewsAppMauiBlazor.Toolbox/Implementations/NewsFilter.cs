using NewsAppMauiBlazor.Toolbox.Interfaces;
using NewsAppMauiBlazor.Domain.Models;

namespace NewsAppMauiBlazor.Toolbox.Implementations
{
    public enum ERuleType
    {
        Whitelist,
        Blacklist,
        NbrOfItems
    }

    public class FilterRule
    {
        public ERuleType ruleType { get; set; }
        public string Value { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public FilterRule(string _value, string _name, ERuleType _ruleType)
        {
            ruleType = _ruleType;
            Value = _value;
            Name = _name;
        }

        public FilterRule()
        {
            
        }

        public bool Check(string input) => ruleType switch
        {
            ERuleType.Whitelist => input.ToLower().Contains(Value.ToLower()),
            ERuleType.Blacklist => !input.ToLower().Contains(Value.ToLower()),
            _ => throw new Exception("Unknown rule")
        };
    }
    public class NewsFilter : IFilter<NewsModel>
    {
        private FilterRule[] rules;
        public NewsFilter(FilterRule[] _rules)
        {
            rules = _rules;
        }
        public bool Catch(NewsModel model)
        {
            foreach(FilterRule rule in rules)
            {
                bool temp = rule.Check(model.Title);
                if (!temp) { return false; }
            }
            return true;
        }
    }
}
