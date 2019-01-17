using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Resolving;
using Autofac.Features.Scanning;
using System;

namespace Framework.Autofac
{
    public class IoCGlobal
    {
        private static IContainer _container;

        public static IContainer Config(Action<ContainerBuilder> configFn)
        {
            var builder = new ContainerBuilder();
            configFn(builder);

            _container = builder.Build();

            return _container;
        }

        public static ILifetimeScope NewScope(string name = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                return _container.BeginLifetimeScope(name);
            }
            else
            {
                return _container.BeginLifetimeScope();
            }
        }

        public static object Resolve(Type type, string name = null, ILifetimeScope scope = null, string tagName = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    if (scope == null)
                    {
                        return _container.Resolve(type);
                    }
                    else
                    {
                        return scope.Resolve(type);
                    }
                }
                else if (string.IsNullOrWhiteSpace(tagName))
                {
                    if (scope == null)
                    {
                        return _container.Resolve(type);
                    }
                    else
                    {
                        return scope.Resolve(type);
                    }
                }
                else
                {
                    if (scope == null)
                    {
                        return _container.ResolveNamed(name, type);
                    }
                    else
                    {
                        return scope.ResolveNamed(name, type);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public static void DefaultTrackOnActive<TLimit>(IActivatedEventArgs<TLimit> eventArgs)
        {
            var instanceLookup = eventArgs.Context as IInstanceLookup;
            if (instanceLookup != null)
            {
                System.Diagnostics.Debug.WriteLine($"IOC ACTIVATION: {instanceLookup.ActivationScope.GetType()} -> {eventArgs.Instance.GetType().FullName}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine($"IOC ACTIVATION: {eventArgs.Context.ComponentRegistry.GetType()} -> {eventArgs.Instance.GetType().FullName}");
            }
        }

        public static void DefaultTrackOnRelease<T>(T instance)
        {
            System.Diagnostics.Debug.WriteLine($"IOC RELEASE: {instance.GetType().FullName}");
        }

        public static void DefaultTrackOnRelease(object instance)
        {
            System.Diagnostics.Debug.WriteLine($"IOC RELEASE: {instance.GetType().FullName}");
        }

        public static void MarkInstanceForDisposal(IDisposable instance)
        {
            _container.Disposer.AddInstanceForDisposal(instance);
        }

        public static T Resolve<T>(string name = null, ILifetimeScope scope = null)
        {
            return (T)Resolve(typeof(T), name, scope);
        }

        public static void DisposeDependency<T>(T dependency) where T : IDisposable
        {
            _container.Disposer.AddInstanceForDisposal(dependency);
        }


    }

    public static class Extensions
    {
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> TrackInstanceEvents(this IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registrationBuilder)
        {
            registrationBuilder.OnActivated(IoCGlobal.DefaultTrackOnActive);

            registrationBuilder.OnRelease(IoCGlobal.DefaultTrackOnRelease);

            return registrationBuilder;
        }

        public static IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> TrackInstanceEvents<T>(this IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> registrationBuilder)
        {
            registrationBuilder.OnActivated(IoCGlobal.DefaultTrackOnActive);

            registrationBuilder.OnRelease(IoCGlobal.DefaultTrackOnRelease);

            return registrationBuilder;
        }
    }
}
