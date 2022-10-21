using ExchangeIt.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace ExchangeIt.Permissions;

public class ExchangeItPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ExchangeItPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(ExchangeItPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ExchangeItResource>(name);
    }
}
