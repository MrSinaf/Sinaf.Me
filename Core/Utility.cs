using System.Buffers.Binary;

namespace Sinaf.Me;

public static class Utility
{
	public static bool GetPNGSize(byte[] bytes, out int width, out int height)
	{
		width = 0;
		height = 0;
		
		if (bytes.Length < 24)
			return false;
		
		var isPng = bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47 &&
					bytes[4] == 0x0D && bytes[5] == 0x0A && bytes[6] == 0x1A && bytes[7] == 0x0A;
		
		if (!isPng)
			return false;
		
		width = BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(16, 4));
		height = BinaryPrimitives.ReadInt32BigEndian(bytes.AsSpan(20, 4));
		return true;
	}
}