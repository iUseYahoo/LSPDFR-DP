using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;

namespace Ambient_FX
{
	public sealed class LoopedPTFX
	{
		public int Handle { get; private set; }

		public List<int> myList { get; private set; }

		public string AssetName { get; private set; }

		public string FXName { get; private set; }

		public bool Exists
		{
			get
			{
				bool flag = this.Handle == -1;
				return !flag && Function.Call<bool>(8408201869211353243L, new InputArgument[]
				{
					this.Handle
				});
			}
		}

		public bool IsLoaded
		{
			get
			{
				return Function.Call<bool>(-8718333986571631532L, new InputArgument[]
				{
					this.AssetName
				});
			}
		}

		public float Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				Hash hash = -5457711104587839187L;
				InputArgument[] array = new InputArgument[2];
				array[0] = this.Handle;
				int num = 1;
				this.scale = value;
				array[num] = value;
				Function.Call(hash, array);
			}
		}

		public LoopedPTFX(string assetName, string fxName)
		{
			this.Handle = -1;
			this.AssetName = assetName;
			this.FXName = fxName;
		}

		public void Load()
		{
			Function.Call(-5184338789570016586L, new InputArgument[]
			{
				this.AssetName
			});
		}

		public void Start(Entity entity, float scale, Vector3 offset, Vector3 rotation, Bone? bone)
		{
			bool flag = this.Handle != -1;
			if (!flag)
			{
				this.scale = scale;
				Function.Call(7798175403732277905L, new InputArgument[]
				{
					this.AssetName
				});
				this.Handle = ((bone == null) ? Function.Call<int>(1937722214304277783L, new InputArgument[]
				{
					this.FXName,
					entity,
					offset.X,
					offset.Y,
					offset.Z,
					rotation.X,
					rotation.Y,
					rotation.Z,
					scale,
					0,
					0,
					1
				}) : Function.Call<int>(-4113118388411728117L, new InputArgument[]
				{
					this.FXName,
					entity,
					offset.X,
					offset.Y,
					offset.Z,
					rotation.X,
					rotation.Y,
					rotation.Z,
					bone.Value,
					scale,
					0,
					0,
					0
				}));
			}
		}

		public void Start(Entity entity, float scale)
		{
			this.Start(entity, scale, Vector3.Zero, Vector3.Zero, null);
		}

		public void Start(Vector3 position, float scale, Vector3 rotation)
		{
			bool flag = this.Handle != -1;
			if (!flag)
			{
				this.scale = scale;
				Function.Call(7798175403732277905L, new InputArgument[]
				{
					this.AssetName
				});
				this.Handle = Function.Call<int>(-2196361402923806489L, new InputArgument[]
				{
					this.FXName,
					position.X,
					position.Y,
					position.Z,
					rotation.X,
					rotation.Y,
					rotation.Z,
					scale,
					0,
					0,
					0,
					0
				});
			}
		}

		public void Start(Vector3 position, float scale)
		{
			this.Start(position, scale, Vector3.Zero);
		}

		public void SetOffsets(Vector3 offset, Vector3 rotOffset)
		{
			Function.Call(-586052976514679741L, new InputArgument[]
			{
				this.Handle,
				offset.X,
				offset.Y,
				offset.Z,
				rotOffset.X,
				rotOffset.Y,
				rotOffset.Z
			});
		}

		public void SetColor(float colorR, float colorG, float colorB, float colorA)
		{
			Function.Call(9191676997121112123L, new InputArgument[]
			{
				this.Handle,
				colorR,
				colorG,
				colorB,
				false
			});
			Function.Call(8243915066403984430L, new InputArgument[]
			{
				this.Handle,
				colorA / 255f
			});
		}

		public void Remove()
		{
			bool flag = this.Handle == -1;
			if (!flag)
			{
				Function.Call(-8109406742613235306L, new InputArgument[]
				{
					this.Handle,
					0
				});
				Function.Call(-4323085940105063473L, new InputArgument[]
				{
					this.Handle,
					0
				});
				this.Handle = -1;
			}
		}

		public void Remove(Vector3 position, float radius)
		{
			bool flag = this.Handle == -1;
			if (!flag)
			{
				Function.Call(-2514703916908317947L, new InputArgument[]
				{
					position.X,
					position.Y,
					position.Z,
					radius
				});
				this.Handle = -1;
			}
		}

		public void Unload()
		{
			bool isLoaded = this.IsLoaded;
			if (isLoaded)
			{
				Function.Call(6873033708056672621L, new InputArgument[]
				{
					this.AssetName
				});
			}
		}

		private float scale;
	}
}
