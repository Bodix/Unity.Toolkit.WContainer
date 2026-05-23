using System.Collections.ObjectModel;
using Bodix.Evolunity.Patterns;
using UnityEngine;
using VContainer;

namespace Toolkit.WContainer
{
	public static class VContainerExtensions
	{
		public static void InjectGameObjectWithoutChildren(this IObjectResolver resolver, GameObject gameObject)
		{
			// TODO: Optimize it with cached list and GetComponents method overload. [#optimization]
			foreach (Component component in gameObject.GetComponents<Component>())
				resolver.Inject(component);
		}

		public static void RegisterOptional<T>(this IContainerBuilder builder, T instance)
		{
			builder.RegisterInstance(new Optional<T>(instance));
		}

		public static void RegisterRuntimeCollection<T>(this IContainerBuilder builder,
			Lifetime lifetime = Lifetime.Scoped)
		{
			builder.Register<ObservableCollection<T>>(lifetime)
				.AsSelf()
				.AsImplementedInterfaces(); // Allows injecting INotifyCollectionChanged if needed.
		}

		// IMPORTANT!!! This approach leads to hidden issues with mutable references.
		// It requires that the reference’s generic type be registered in the container.
		// And if it is registered in the container, there is a risk of accidentally
		// using the type itself directly (instead of using a reference to it).
		//
		// public static void RegisterMutableReference<T>(this IContainerBuilder builder,
		// 	Lifetime lifetime = Lifetime.Scoped) where T : class
		// {
		// 	builder.Register<ObservableReference<T>>(lifetime);
		// }

		public static void RegisterMutableReference<T>(this IContainerBuilder builder, T instance,
			Lifetime lifetime = Lifetime.Scoped) where T : class
		{
			builder.RegisterInstance(new ObservableReference<T>(instance));
		}
	}
}