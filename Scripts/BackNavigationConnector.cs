using Bodix.Evolunity.Services;
using UnityEngine;
using VContainer;

namespace Toolkit.WContainer
{
	[RequireComponent(typeof(IBackHandler))]
	public class BackNavigationConnector : MonoBehaviour
	{
		[Inject]
		private IBackNavigationService _navigationService;

		private IBackHandler _handler;

		private void Awake()
		{
			_handler = GetComponent<IBackHandler>();
		}

		private void OnEnable()
		{
			_navigationService?.Register(_handler);
		}

		private void OnDisable()
		{
			_navigationService?.Unregister(_handler);
		}
	}
}