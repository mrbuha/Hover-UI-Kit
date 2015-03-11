﻿using System;
using Hoverboard.Core.Display.Default;
using UnityEngine;

namespace Hoverboard.Demo {

	/*================================================================================================*/
	public class DemoTextPixels {

		private readonly UiLabel vUiLabel;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public DemoTextPixels(UiLabel pLabel) {
			vUiLabel = pLabel;
			vUiLabel.SetSize(1, 1);
			vUiLabel.FontName = "TahomaPixel16";
			vUiLabel.Alpha = 1;
			vUiLabel.Color = Color.red;
			vUiLabel.FontSize = 40;
			vUiLabel.Label = "Test";
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public float[,] GetPixels(char pLetter, out int pWidth, out int pHeight) {
			Texture2D tex = vUiLabel.Texture;
			//string data = "";
			CharacterInfo charInfo;

			if ( !vUiLabel.FontObject.GetCharacterInfo(pLetter, out charInfo) ) {
				throw new Exception("Character pixels not found: "+pLetter);
			}

			int x = (int)Math.Round(charInfo.uv.x*tex.width);
			int y = (int)Math.Round(charInfo.uv.y*tex.height);
			int w = (int)Math.Round(charInfo.uv.width*tex.width);
			int h = (int)Math.Round(charInfo.uv.height*tex.height);

			if ( w < 0 ) {
				w *= -1;
				x -= w;
			}

			if ( h < 0 ) {
				h *= -1;
				y -= h;
			}

			var pixels = new float[w, h];

			for ( int hi = y ; hi < y+h ; ++hi ) {
				for ( int wi = x ; wi < x+w ; ++wi ) {
					float a = tex.GetPixel(wi, hi).a;
					pixels[wi-x, hi-y] = a;
					//data += (a < 0.25f ? " " : (a < 0.5f ? "." : (a < 0.75f ? "*" : "#")));
				}

				//data += "\n";
			}

			//Debug.Log(x+" / "+y+" / "+w+" / "+h+"\n"+data);
			pWidth = w;
			pHeight = h;
			return pixels;
		}

	}

}
