using Bodix.Evolunity.Services;
using VContainer;

namespace Toolkit.WContainer
{
	public class ExitHandler : Bodix.Evolunity.Components.ExitHandler
	{
		[Inject]
		private UiDialogService _uiDialogService;

		protected override UiDialogService UiDialogService => _uiDialogService;
	}
}