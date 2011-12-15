[assembly: WebActivator.PreApplicationStartMethod(typeof(abSee.Commands.App_Start.NetBashStart), "Start")] 
namespace abSee.Commands.App_Start {
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