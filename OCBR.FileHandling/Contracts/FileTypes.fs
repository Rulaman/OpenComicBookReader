namespace OCBR.FileHandling

open System.Drawing

type Translation = {
    Name: string
    LanguageCode: string
    SvgContent: string
}

type LayerInfo = {
    Image: Bitmap;
    Translations: Translation list
    Name: string;
    Visible: bool;
    X: int;
    Y: int;
}

type OcbFileInfo = {
    Width: int
    Height: int
    Layers: LayerInfo list
}
