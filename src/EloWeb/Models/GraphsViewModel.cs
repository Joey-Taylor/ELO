using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EloWeb.Models
{
    public class GraphsViewModel
    {
        public List<CheckBoxViewModel> Names { get; set; }
        public GraphType GraphType { get; set; }
    }

    public enum GraphType
    {
        [Display(Name = "ELO vs Games")]
        [Description("SelectedEloByGames")]
        EloVsGames,

        [Display(Name = "ELO vs Time")]
        [Description("SelectedEloByTime")]
        EloVsTime
    }

    public static class GraphTypeExtensions
    {
        public static string ToDisplayString(this GraphType enumValue)
        {
            var display = enumValue.GetType()
                           .GetMember(enumValue.ToString()).First()
                           .GetCustomAttributes(false)
                           .OfType<DisplayAttribute>()
                           .LastOrDefault();

            return display != null ? display.GetName() : enumValue.ToString();
        }

        public static string ToActionName(this GraphType enumValue)
        {
            var description = enumValue.GetType()
                           .GetMember(enumValue.ToString()).First()
                           .GetCustomAttributes(false)
                           .OfType<DescriptionAttribute>()
                           .Last();

            return description.Description;
        }
    }
}