#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2021                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

using System.Xml;

namespace Zeiss.PiWeb.ColorScale;

/// <summary>
/// Helper class for reading and writing data into xml files or binary streams.
/// </summary>
public static class ReaderWriterExtensions
{
	#region methods

	/// <summary>
	/// Writes the <paramref name="color"/> as an hex string at an attribute named <paramref name="name"/>.
	/// </summary>
	internal static void WriteColorAttribute( this XmlWriter writer, string name, Color color )
	{
		writer.WriteAttributeString( name, $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}" );
	}
		
	/// <summary>
	/// Reads a hex color value from an attribute with name <paramref name="name"/>.
	/// </summary>
	public static Color ReadColorAttribute( this XmlReader reader, string name )
	{
		var value = reader.GetAttribute( name );
		return Color.ParseArgb( value );
	}

	#endregion
}