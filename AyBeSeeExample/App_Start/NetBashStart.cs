[assembly: WebActivator.PreApplicationStartMethod(typeof(AyBeSeeExample.App_Start.NetBashStart), "Start")] 
namespace AyBeSeeExample.App_Start {
	using NetBash;
    public static class NetBashStart {
        public static void Start() {
			NetBash.Init();
			
			//TODO: replace with your own auth code
			//NetBash.Settings.Authorize = (request) =>
			//	{
			//		return request.IsLocal;
			//	};
        }
    }
}