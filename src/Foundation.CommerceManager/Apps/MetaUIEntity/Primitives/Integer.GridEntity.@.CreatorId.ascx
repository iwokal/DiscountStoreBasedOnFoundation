<%@ Control Language="C#" AutoEventWireup="true" Inherits="Mediachase.BusinessFoundation.BaseEntityType" ClassName="Mediachase.Ibn.Web.UI.MetaUI.EntityPrimitives.Integer_GridEntity_All_CreatorId"%>
<%@ Import Namespace="Mediachase.UI.Web.Util" %>
<%@ Import Namespace="Mediachase.BusinessFoundation.Data.Business" %>
<script language="c#" runat="server">
	protected string GetValue(EntityObject DataItem, string FieldName)
	{
		string retVal = "";

		if (DataItem != null && DataItem.Properties[FieldName] != null && DataItem[FieldName] != null)
		{
			retVal = CHelper.GetUserName((int)DataItem[FieldName]);
		}
		return retVal;
	}
</script>
<%# GetValue(DataItem, FieldName) %>
