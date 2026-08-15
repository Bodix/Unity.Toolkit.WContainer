using Bodix.Evolunity.Services;
using UnityEngine;
using VContainer;

namespace Toolkit.WContainer
{
	[RequireComponent(typeof(IBackNavigationHandler))]
	public class BackNavigationConnector : MonoBehaviour
	{
		/*
		 * NOTE: When this connector might NOT be suitable.
		 *
		 * This connector relies on the standard Unity GameObject lifecycle (OnEnable / OnDisable)
		 * to register and unregister the back navigation handler. While this works perfectly for
		 * 90% of standard UI elements that are toggled via GameObject.SetActive(), there are
		 * specific scenarios where you should NOT use this connector:
		 *
		 * 1. CanvasGroup Optimizations (Single Active GameObject):
		 *    If your UI avoids SetActive() to prevent Canvas rebuilding and instead toggles
		 *    visibility using CanvasGroup.alpha and interactable properties, OnEnable() will
		 *    only fire once. You must manage registration manually based on alpha or animation state.
		 *
		 * 2. Non-MonoBehaviour Handlers (Pure C# Classes):
		 *    If you are using a state machine (e.g., ExplorationState, InventoryState) where
		 *    states are standard C# classes without Unity lifecycle methods, you cannot use this.
		 *    Registration must happen dynamically upon state transitions.
		 *
		 * 3. Temporary Input Blocking (Tutorials or Loading Screens):
		 *    If a window must not be closed for a specific duration (e.g., forcing the user to
		 *    read text for 5 seconds), relying on OnEnable is not enough. The window must register
		 *    itself, return true in OnBackPressed() to consume the input without closing, and
		 *    then handle the actual closing logic after the timer expires.
		 *
		 * 4. Nested Logical States Within a Single Window:
		 *    For complex screens (like character creation: Race -> Class -> Face) where the
		 *    GameObject remains active but the back button should navigate between sub-steps.
		 *    The screen must dynamically register new handlers or change its internal logic.
		 *
		 * HOW TO HANDLE THESE CASES:
		 * Do not use the BackNavigationConnector. Instead, inject the IBackNavigationService
		 * directly into your complex class (State Machine, CanvasGroup Manager, or Custom Controller).
		 * Call the Register() and Unregister() methods manually according to your custom business logic.
		 */

		[Inject]
		private IBackNavigationService _navigationService;

		private IBackNavigationHandler _handler;

		private void Awake()
		{
			_handler = GetComponent<IBackNavigationHandler>();
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