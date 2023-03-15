using System;

namespace PersistentWeaponFlashlight.Memory
{
	
	internal class Nopper
	{
		
		
		public IntPtr Address { get; }

		
		
		public int BytesCount { get; }

		
		
		
		public byte[] OriginalBytes { get; private set; }

		
		
		
		public bool IsNopped { get; private set; }

		
		public Nopper(IntPtr address, int bytesCount)
		{
			this.Address = address;
			this.BytesCount = bytesCount;
		}

		
		public unsafe void Nop()
		{
			if (this.IsNopped)
			{
				throw new InvalidOperationException("Already nopped");
			}
			this.OriginalBytes = new byte[this.BytesCount];
			for (int i = 0; i < this.BytesCount; i++)
			{
				this.OriginalBytes[i] = *(byte*)((void*)(this.Address + i));
				*(byte*)((void*)(this.Address + i)) = 144;
			}
			this.IsNopped = true;
		}

		
		public unsafe void Restore()
		{
			if (!this.IsNopped)
			{
				throw new InvalidOperationException("Not nopped");
			}
			for (int i = 0; i < this.BytesCount; i++)
			{
				*(byte*)((void*)(this.Address + i)) = this.OriginalBytes[i];
			}
			this.OriginalBytes = null;
			this.IsNopped = false;
		}

		
		public const int NopOpcode = 144;
	}
}
