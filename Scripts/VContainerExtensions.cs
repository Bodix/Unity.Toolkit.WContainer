using System.Collections.ObjectModel;
using Bodix.Evolunity.Patterns;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Toolkit.WContainer
{
	public static class VContainerExtensions
	{
		public static void InjectGameObjectWithoutChildren(this IObjectResolver objectResolver, GameObject gameObject)
		{
			// TODO: Optimize it with cached list and GetComponents method overload. [#optimization]
			foreach (Component component in gameObject.GetComponents<Component>())
				objectResolver.Inject(component);
		}

		public static void RegisterInstanceOptional<T>(this IContainerBuilder containerBuilder, T instance)
		{
			containerBuilder.RegisterInstance(new Optional<T>(instance));
		}

		public static void RegisterComponentOptional<T>(this IContainerBuilder containerBuilder, T instance)
		{
			containerBuilder.RegisterComponent(new Optional<T>(instance));
		}

		public static void RegisterRuntimeCollection<T>(this IContainerBuilder builder,
			Lifetime lifetime = Lifetime.Scoped)
		{
			builder.Register<ObservableCollection<T>>(lifetime)
				.AsSelf()
				.AsImplementedInterfaces(); // Allows injecting INotifyCollectionChanged if needed.
		}

		public static void RegisterMutableReference<T>(this IContainerBuilder builder,
			Lifetime lifetime = Lifetime.Scoped) where T : class
		{
			builder.Register<ObservableReference<T>>(lifetime);
		}

		public static void RegisterMutableReference<T>(this IContainerBuilder builder, T instance,
			Lifetime lifetime = Lifetime.Scoped) where T : class
		{
			builder.RegisterInstance(new ObservableReference<T>(instance));
		}
	}
}