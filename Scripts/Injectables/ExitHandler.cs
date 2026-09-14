using PerfectCore.PerfectUI;
using UnityEngine;
using VContainer;

namespace Toolkit.WContainer
{
	[RequireComponent(typeof(BackNavigationConnector))]
	public class ExitHandler : PerfectCore.PerfectUI.ExitHandler
	{
		[Inject]
		private UiDialogService _uiDialogService;

		protected override UiDialogService UiDialogService => _uiDialogService;
	}
}