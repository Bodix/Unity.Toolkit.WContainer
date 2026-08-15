using Bodix.Evolunity.Services;
using UnityEngine;
using VContainer;

namespace Toolkit.WContainer
{
	[RequireComponent(typeof(BackNavigationConnector))]
	public class ExitHandler : Bodix.Evolunity.Components.ExitHandler
	{
		[Inject]
		private UiDialogService _uiDialogService;

		protected override UiDialogService UiDialogService => _uiDialogService;
	}
}