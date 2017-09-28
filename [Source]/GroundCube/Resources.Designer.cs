using System.Resources;


namespace PQSMod_GroundCube
{
    internal class Resources
    {
        private static ResourceManager resourceMan;

        internal Resources()
        {
        }

        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    ResourceManager temp = new ResourceManager("PQSMod_GroundCube.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        internal static byte[] SigmaGCLS_1
        {
            get
            {
                object obj = ResourceManager.GetObject("SigmaGCLS_1");
                return ((byte[])(obj));
            }
        }
    }
}
