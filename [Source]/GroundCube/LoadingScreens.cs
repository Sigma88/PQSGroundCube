using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;


namespace PQSMod_GroundCube
{
    [KSPAddon(KSPAddon.Startup.Instantly, true)]
    public class LoadingScreens : MonoBehaviour
    {
        bool skip = false;
        Texture2D texture;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {
            // Load the Texture
            texture = LoadDDS(Resources.SigmaGCLS_1);
        }

        void Update()
        {
            // Wait for the Loading Screens to be available
            if (!skip && LoadingScreen.Instance?.Screens?.Skip(1)?.FirstOrDefault() != null)
            {
                LoadScreen(LoadingScreen.Instance?.Screens?.Skip(1)?.FirstOrDefault());
                skip = true;
            }

            if (HighLogic.LoadedScene == GameScenes.MAINMENU)
            {
                DestroyImmediate(this);
            }
        }

        void LoadScreen(LoadingScreen.LoadingScreenState screen)
        {
            // Add the texture to the Loading Screens
            List<Object> newScreens = screen.screens.ToList();
            newScreens.Add(texture);
            screen.screens = newScreens.ToArray();
        }

        Texture2D LoadDDS(byte[] bytes)
        {
            if (bytes[4] != 124) return null; //this byte should be 124 for DDS image files

            int height = bytes[13] * 256 + bytes[12];
            int width = bytes[17] * 256 + bytes[16];

            int header = 128;
            byte[] data = new byte[bytes.Length - header];
            Buffer.BlockCopy(bytes, header, data, 0, bytes.Length - header);

            Texture2D texture = new Texture2D(width, height, TextureFormat.DXT5, false);
            texture.LoadRawTextureData(data);
            texture.Apply();

            return (texture);
        }
    }
}
