using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Utilities
{
    public class ScreenshotHandler : MonoBehaviour
    {
        [Title("// General")]
        [SerializeField, Range(1, 9)] int _screenshotScale = 1;
        //[SerializeField] bool _takeTransparentScreenshot = false;
        [SerializeField] bool _changeTimeScale = false;
        [SerializeField, Range(0f, 1f)] float _timeScale = 1f;

        private void Start()
        {
            TryCreateDirectory();

            //if (_takeTransparentScreenshot)
            //{
            //    TakeTransparentScreenshot();
            //}
        }

        private void Update()
        {
            if (_changeTimeScale)
            {
                Time.timeScale = _timeScale;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //TakeScreenshot();
                TakeTransparentScreenshot("MyTransparentScreenshot");
            }
        }

        [Button]
        public void TakeScreenshot()
        {
            string _filename = GetFileName();
            ScreenCapture.CaptureScreenshot(_filename, _screenshotScale);
            Debug.Log($"// {_filename} Taken!");
        }

        public void TakeTransparentScreenshot()
        {
            string _filename = GetFileName();
            TakeTransparentScreenshot(Camera.main, Screen.width * _screenshotScale, Screen.height * _screenshotScale, _filename);
            Debug.Log($"// {_filename} Taken!");
        }

        private void TakeTransparentScreenshot(Camera cam, int width, int height, string savePath)
        {
            // Depending on your render pipeline, this may not work.
            var bak_cam_targetTexture = cam.targetTexture;
            var bak_cam_clearFlags = cam.clearFlags;
            var bak_RenderTexture_active = RenderTexture.active;

            var tex_transparent = new Texture2D(width, height, TextureFormat.ARGB32, false);
            // Must use 24-bit depth buffer to be able to fill background.
            var render_texture = RenderTexture.GetTemporary(width, height, 24, RenderTextureFormat.ARGB32);
            var grab_area = new Rect(0, 0, width, height);

            RenderTexture.active = render_texture;
            cam.targetTexture = render_texture;
            cam.clearFlags = CameraClearFlags.SolidColor;

            // Simple: use a clear background
            cam.backgroundColor = Color.clear;
            cam.Render();
            tex_transparent.ReadPixels(grab_area, 0, 0);
            tex_transparent.Apply();

            // Encode the resulting output texture to a byte array then write to the file
            byte[] pngShot = ImageConversion.EncodeToPNG(tex_transparent);
            File.WriteAllBytes(savePath, pngShot);

            cam.clearFlags = bak_cam_clearFlags;
            cam.targetTexture = bak_cam_targetTexture;
            RenderTexture.active = bak_RenderTexture_active;
            RenderTexture.ReleaseTemporary(render_texture);
            Destroy(tex_transparent);
        }

        private void TryCreateDirectory()
        {
            if (!Directory.Exists("Assets/Screenshots"))
            {
                Directory.CreateDirectory("Assets/Screenshots");
            }
        }

        private string GetFileName()
        {
            return string.Format("Assets/Screenshots/capture_{0}.png", DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff"));
        }

        public Camera captureCamera; // Assign your capture camera here
        public RenderTexture targetRenderTexture; // Assign your render texture here

        public void TakeTransparentScreenshot(string fileName)
        {
            if (captureCamera == null || targetRenderTexture == null)
            {
                Debug.LogError("Capture camera or target render texture not assigned!");
                return;
            }

            // Set the active render texture to our target
            RenderTexture.active = targetRenderTexture;

            // Render the camera view to the render texture
            captureCamera.Render();

            // Create a new Texture2D to read the pixels
            Texture2D screenshot = new Texture2D(targetRenderTexture.width, targetRenderTexture.height, TextureFormat.ARGB32, false);
            screenshot.ReadPixels(new Rect(0, 0, targetRenderTexture.width, targetRenderTexture.height), 0, 0);
            screenshot.Apply();

            // Encode to PNG and save
            byte[] bytes = screenshot.EncodeToPNG();
            string filePath = Path.Combine(Application.dataPath, fileName + ".png");
            File.WriteAllBytes(filePath, bytes);

            Debug.Log("Transparent screenshot saved to: " + filePath);

            // Reset the active render texture
            RenderTexture.active = null;
        }
    }
}
