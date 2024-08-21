using CMS.Membership;

using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.UIPages;

using XperienceCommunity.Tasks.Admin;

[assembly: UIApplication(
    identifier: TaskApplicationPage.IDENTIFIER,
    type: typeof(TaskApplicationPage),
    slug: "tasks",
    name: "Tasks",
    category: BaseApplicationCategories.DEVELOPMENT,
    icon: Icons.TimedBox,
    templateName: TemplateNames.SECTION_LAYOUT)]

namespace XperienceCommunity.Tasks.Admin;

[UIPermission(SystemPermissions.VIEW)]
[UIPermission(SystemPermissions.CREATE)]
[UIPermission(SystemPermissions.UPDATE)]
[UIPermission(SystemPermissions.DELETE)]
internal class TaskApplicationPage : ApplicationPage
{
    public const string IDENTIFIER = "XperienceCommunity.Tasks.Admin";
}
