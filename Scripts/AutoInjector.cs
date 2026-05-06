using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Toolkit.WContainer
{
	public class AutoInjector : MonoBehaviour
	{
		[SerializeField]
		private InjectionMethod _injectionMethod;
		[SerializeField]
		private ParentReference _targetLifetimeScope;

		private void Awake()
		{
			Inject();
		}

		private void Inject()
		{
			if (_targetLifetimeScope.Object != null)
			{
				InjectWithSelectedMethod(_targetLifetimeScope.Object.Container);
			}
			else
			{
				LifetimeScope scope = FindLifetimeScope(_targetLifetimeScope.Type);
				if (scope != null)
					InjectWithSelectedMethod(scope.Container);
				else
					Debug.LogError(nameof(LifetimeScope) + " not found");
			}
		}

		private void InjectWithSelectedMethod(IObjectResolver container)
		{
			switch (_injectionMethod)
			{
				default:
				case InjectionMethod.GameObject:
					container.InjectGameObject(gameObject);
					break;
				case InjectionMethod.GameObjectWithoutChildren:
					container.InjectGameObjectWithoutChildren(gameObject);
					break;
			}
		}

		private enum InjectionMethod
		{
			GameObject,
			GameObjectWithoutChildren
		}

		private static LifetimeScope FindLifetimeScope(Type type)
		{
#if UNITY_2022_1_OR_NEWER
			return (LifetimeScope)FindAnyObjectByType(type);
#else
			return (LifetimeScope)FindObjectOfType(type);
#endif
		}
	}
}