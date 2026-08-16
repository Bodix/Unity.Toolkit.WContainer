using Bodix.Evolunity.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Toolkit.WContainer
{
	public class VContainerInstantiator : IInstantiator
	{
		private readonly IObjectResolver _resolver;

		public VContainerInstantiator(IObjectResolver resolver)
		{
			_resolver = resolver;
		}

		public T Instantiate<T>(T prefab, Transform parent) where T : Component
		{
			return _resolver.Instantiate(prefab, parent);
		}
	}
}