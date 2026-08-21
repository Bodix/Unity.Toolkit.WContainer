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
		 * This connector relies strictly on the Unity GameObject lifecycle (OnEnable / OnDisable)
		 * to register and unregister the back navigation handler. It works perfectly for
		 * standard UI elements toggled via GameObject.SetActive().
		 *
		 * Do NOT use this connector in the following scenarios:
		 *
		 * 1. Hierarchy Race Conditions (Parent-Child Ambiguity):
		 *    Unity does NOT guarantee the execution order of OnEnable() between a parent and its
		 *    child GameObjects when they are activated simultaneously (e.g., SetActive(true) on the parent).
		 *    On PC, the child might register last (getting priority). On Android, the parent might
		 *    register last, breaking the expected LIFO back-button behavior.
		 *    Fix: Remove this connector from child elements. Put it only on the parent window,
		 *    and let the parent's OnBackPressed() manually delegate the call to its children.
		 *
		 * 2. CanvasGroup Optimizations (Avoidance of SetActive):
		 *    If your UI avoids SetActive() to prevent Canvas rebuilding and instead toggles
		 *    visibility using CanvasGroup.alpha and interactable properties, OnEnable() will
		 *    only fire once. You must manage registration manually based on alpha/state changes.
		 *
		 * 3. Non-MonoBehaviour Handlers (Pure C# Classes):
		 *    If you are using a state machine (e.g., ExplorationState) where states are standard
		 *    C# classes without Unity lifecycle methods, registration must happen dynamically.
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