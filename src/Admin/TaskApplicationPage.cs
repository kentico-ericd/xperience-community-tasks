using CMS.Membership;

using Kentico.Xperience.Admin.Base;
using Kentico.Xperience.Admin.Base.UIPages;

using Xperience.Community.Tasks.Admin;

[assembly: UIApplication(
    identifier: TaskApplicationPage.IDENTIFIER,
    type: typeof(TaskApplicationPage),
    slug: "tasks",
    name: "Tasks",
    category: BaseApplicationCategories.DEVELOPMENT,
    icon: Icons.TimedBox,
    templateName: TemplateNames.SECTION_LAYOUT)]

namespace Xperience.Community.Tasks.Admin;

[UIPermission(SystemPermissions.VIEW)]
[UIPermission(SystemPermissions.CREATE)]
[UIPermission(SystemPermissions.UPDATE)]
[UIPermission(SystemPermissions.DELETE)]
internal class TaskApplicationPage : ApplicationPage
{
    public const string IDENTIFIER = "Xperience.Community.Tasks.Admin";
}
