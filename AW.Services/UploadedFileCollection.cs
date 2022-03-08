using System;
using System.Collections.Generic;

namespace AW.Services
{
	/// <summary>
	/// A collection of uploaded files
	/// </summary>
	public class UploadedFileCollection : List<UploadedFile>, IDisposable
	{
		#region IDisposable Members

		/// <summary>
		/// Disposes of each file contained in this collection
		/// </summary>
		public void Dispose()
		{
			foreach (var file in this)
				file.Dispose();
		}

		#endregion
	}
}