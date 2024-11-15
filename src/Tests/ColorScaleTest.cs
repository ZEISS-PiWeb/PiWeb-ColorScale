#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2024                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion

namespace Zeiss.PiWeb.ColorScale.Tests;

#region usings

using System;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;

#endregion

[TestFixture]
public class ColorScaleTest
{
	#region methods

	[Test]
	public void Test_Initialization_With_No_Entries()
	{
		Assert.That( () => new ColorScale( "", Color.FromRgb( 0, 0, 0 ), Array.Empty<ColorScaleEntry>() ), Throws.ArgumentException );
	}

	[Test]
	public void Test_Get_Color_With_Single_Entry()
	{
		var invalidColor = Color.FromRgb( 128, 128, 128 );
		var leftColor = Color.FromRgb( 255, 0, 0 );
		var rightColor = Color.FromRgb( 0, 0, 255 );
		var colorScale = new ColorScale( "", invalidColor, new[] { new ColorScaleEntry( 0, leftColor, rightColor ) } );
		Assert.That( colorScale.GetColor( 1 ), Is.EqualTo( rightColor ) );
		Assert.That( colorScale.GetColor( -1 ), Is.EqualTo( leftColor ) );
	}

	[Test]
	public void Test_Serialization()
	{
		var colorScale = new ColorScale( "foo", Color.FromRgb( 128, 128, 128 ), new[]
		{
			new ColorScaleEntry( 0, Color.FromRgb( 128, 0, 0 ), Color.FromRgb( 255, 0, 0 ) ),
			new ColorScaleEntry( 0, Color.FromRgb( 255, 0, 0 ), Color.FromRgb( 0, 0, 255 ) ),
			new ColorScaleEntry( 0, Color.FromRgb( 0, 0, 255 ), Color.FromRgb( 0, 0, 128 ) )
		} );
		var stream = new MemoryStream();
		var writer = new XmlTextWriter( stream, Encoding.UTF8 );

		writer.WriteStartElement( "ColorScale" );
		colorScale.Write( writer );
		writer.WriteEndElement();
		writer.Flush();

		stream.Seek( 0, SeekOrigin.Begin );

		using var reader = new XmlTextReader( stream );

		reader.Read();
		var clone = ColorScale.Read( reader );

		Assert.That( clone.Name, Is.EqualTo( colorScale.Name ) );
		Assert.That( clone.InvalidColor, Is.EqualTo( colorScale.InvalidColor ) );
		Assert.That( clone.Entries.Length, Is.EqualTo( colorScale.Entries.Length ) );
		Assert.That( clone.Interpolation, Is.EqualTo( colorScale.Interpolation ) );
	}

	#endregion
}