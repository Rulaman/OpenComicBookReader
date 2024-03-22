namespace OCBR.FileHandling

open System.IO
open ICSharpCode.SharpZipLib.Zip
open System.Xml

// This is based on the openraster reading and writing code from
// https://github.com/ArtemiuszPalla/OpenRaster/tree/master/OpenRaster
// modified to fit the needs for the translations part of the open comic book format
module File =
    let fileType = "image/opencomicbook"
    let fileEnding = "ocb"
    let Layers: LayerInfo list = []

    let Save filePath =
        let fileStream = new FileStream(filePath, FileMode.Create)
        let zipStream = new ZipOutputStream(fileStream)
        zipStream.UseZip64 <- UseZip64.Off

        // write the mimetype
        let mimetype = new ZipEntry("mimetype")
        mimetype.CompressionMethod <- CompressionMethod.Stored
        zipStream.PutNextEntry(mimetype)

        let mimebytes = System.Text.Encoding.ASCII.GetBytes(fileType);
        zipStream.Write(mimebytes, 0, mimebytes.Length)

        // write layer information

    ///<summary>Loads an open comic book file with at least one translation</summary>
    ///<param name="filepath">The complete filePath of the file to open.</param>
    ///<return>An layer info object with information about the file</return>
    let Load (filepath: string) =
        let file = new ZipFile(filepath)
        let stackXml = new XmlDocument ()
        stackXml.Load(file.GetInputStream(file.GetEntry("stack.xml")))

        let imageElement = stackXml.DocumentElement
        let imageSize: OcbFileInfo = {
            Width = imageElement.GetAttribute("w") |> int
            Height = imageElement.GetAttribute("h") |> int
            Layers = []
        }

        ignore
