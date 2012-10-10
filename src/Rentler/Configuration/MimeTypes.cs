using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Configuration
{
    public sealed class MimeTypes
    {
        private static volatile MimeTypes instance;
        private static object syncRoot = new Object();

        Dictionary<string, string> types;

        public Dictionary<string, string> Types
        {
            get { return types; }
            set { types = value; }
        }

        private MimeTypes()
        {
            types = new Dictionary<string, string>();

            types["acx"] = "application/internet-property-stream";
            types[".323"] = "text/h323";
            types[".ai"] = "application/postscript";
            types[".aif"] = "audio/x-aiff";
            types[".aifc"] = "audio/x-aiff";
            types[".aiff"] = "audio/x-aiff";
            types[".asf"] = "video/x-ms-asf";
            types[".asr"] = "video/x-ms-asf";
            types[".asx"] = "video/x-ms-asf";
            types[".au"] = "audio/basic";
            types[".avi"] = "video/x-msvideo";
            types[".axs"] = "application/olescript";
            types[".bas"] = "text/plain";
            types[".bcpio"] = "application/x-bcpio";
            types[".bin"] = "application/octet-stream";
            types[".bmp"] = "image/bmp";
            types[".c"] = "text/plain";
            types[".cat"] = "application/vnd.ms-pkiseccat";
            types[".cdf"] = "application/x-cdf";
            types[".cer"] = "application/x-x509-ca-cert";
            types[".class"] = "application/octet-stream";
            types[".clp"] = "application/x-msclip";
            types[".cmx"] = "image/x-cmx";
            types[".cod"] = "image/cis-cod";
            types[".cpio"] = "application/x-cpio";
            types[".crd"] = "application/x-mscardfile";
            types[".crl"] = "application/pkix-crl";
            types[".crt"] = "application/x-x509-ca-cert";
            types[".csh"] = "application/x-csh";
            types[".css"] = "text/css";
            types[".dcr"] = "application/x-director";
            types[".der"] = "application/x-x509-ca-cert";
            types[".dir"] = "application/x-director";
            types[".dll"] = "application/x-msdownload";
            types[".dms"] = "application/octet-stream";
            types[".doc"] = "application/msword";
            types[".dot"] = "application/msword";
            types[".dvi"] = "application/x-dvi";
            types[".dxr"] = "application/x-director";
            types[".eps"] = "application/postscript";
            types[".etx"] = "text/x-setext";
            types[".evy"] = "application/envoy";
            types[".exe"] = "application/octet-stream";
            types[".fif"] = "application/fractals";
            types[".flr"] = "x-world/x-vrml";
            types[".gif"] = "image/gif";
            types[".gtar"] = "application/x-gtar";
            types[".gz"] = "application/x-gzip";
            types[".h"] = "text/plain";
            types[".hdf"] = "application/x-hdf";
            types[".hlp"] = "application/winhlp";
            types[".hqx"] = "application/mac-binhex40";
            types[".hta"] = "application/hta";
            types[".htc"] = "text/x-component";
            types[".htm"] = "text/html";
            types[".html"] = "text/html";
            types[".htt"] = "text/webviewhtml";
            types[".ico"] = "image/x-icon";
            types[".ief"] = "image/ief";
            types[".iii"] = "application/x-iphone";
            types[".ins"] = "application/x-internet-signup";
            types[".isp"] = "application/x-internet-signup";
            types[".jfif"] = "image/pipeg";
            types[".jpe"] = "image/jpeg";
            types[".jpeg"] = "image/jpeg";
            types[".jpg"] = "image/jpeg";
            types[".js"] = "application/x-javascript";
            types[".latex"] = "application/x-latex";
            types[".lha"] = "application/octet-stream";
            types[".lsf"] = "video/x-la-asf";
            types[".lsx"] = "video/x-la-asf";
            types[".lzh"] = "application/octet-stream";
            types[".m13"] = "application/x-msmediaview";
            types[".m14"] = "application/x-msmediaview";
            types[".m3u"] = "audio/x-mpegurl";
            types[".man"] = "application/x-troff-man";
            types[".mdb"] = "application/x-msaccess";
            types[".me"] = "application/x-troff-me";
            types[".mht"] = "message/rfc822";
            types[".mhtml"] = "message/rfc822";
            types[".mid"] = "audio/mid";
            types[".mny"] = "application/x-msmoney";
            types[".mov"] = "video/quicktime";
            types[".movie"] = "video/x-sgi-movie";
            types[".mp2"] = "video/mpeg";
            types[".mp3"] = "audio/mpeg";
            types[".mp4"] = "video/mpeg";
            types[".mpa"] = "video/mpeg";
            types[".mpe"] = "video/mpeg";
            types[".mpeg"] = "video/mpeg";
            types[".mpg"] = "video/mpeg";
            types[".mpp"] = "application/vnd.ms-project";
            types[".mpv2"] = "video/mpeg";
            types[".ms"] = "application/x-troff-msv";
            types[".mvb"] = "application/x-msmediaview";
            types[".nws"] = "message/rfc822";
            types[".oda"] = "application/oda";
            types[".p10"] = "application/pkcs10";
            types[".p12"] = "application/x-pkcs12";
            types[".p7b"] = "application/x-pkcs7-certificates";
            types[".p7c"] = "application/x-pkcs7-mime";
            types[".p7m"] = "application/x-pkcs7-mime";
            types[".p7r"] = "application/x-pkcs7-certreqresp";
            types[".p7s"] = "application/x-pkcs7-signature";
            types[".pbm"] = "image/x-portable-bitmap";
            types[".pdf"] = "application/pdf";
            types[".pfx"] = "application/x-pkcs12";
            types[".pgm"] = "image/x-portable-graymap";
            types[".pko"] = "application/ynd.ms-pkipko";
            types[".pma"] = "application/x-perfmon";
            types[".pmc"] = "application/x-perfmon";
            types[".pml"] = "application/x-perfmon";
            types[".pmr"] = "application/x-perfmon";
            types[".pmw"] = "application/x-perfmon";
            types[".pnm"] = "image/x-portable-anymap";
            types[".png"] = "image/png";
            types[".pot,"] = "application/vnd.ms-powerpoint";
            types[".ppm"] = "image/x-portable-pixmap";
            types[".pps"] = "application/vnd.ms-powerpoint";
            types[".ppt"] = "application/vnd.ms-powerpoint";
            types[".prf"] = "application/pics-rules";
            types[".ps"] = "application/postscript";
            types[".pub"] = "application/x-mspublisher";
            types[".qt"] = "video/quicktime";
            types[".ra"] = "audio/x-pn-realaudio";
            types[".ram"] = "audio/x-pn-realaudio";
            types[".ras"] = "image/x-cmu-raster";
            types[".rgb"] = "image/x-rgb";
            types[".rmi"] = "audio/mid";
            types[".roff"] = "application/x-troff";
            types[".rtf"] = "application/rtf";
            types[".rtx"] = "text/richtext";
            types[".scd"] = "application/x-msschedule";
            types[".sct"] = "text/scriptlet";
            types[".setpay"] = "application/set-payment-initiation";
            types[".setreg"] = "application/set-registration-initiation";
            types[".sh"] = "application/x-sh";
            types[".shar"] = "application/x-shar";
            types[".sit"] = "application/x-stuffit";
            types[".snd"] = "audio/basic";
            types[".spc"] = "application/x-pkcs7-certificates";
            types[".spl"] = "application/futuresplash";
            types[".src"] = "application/x-wais-source";
            types[".sst"] = "application/vnd.ms-pkicertstore";
            types[".stl"] = "application/vnd.ms-pkistl";
            types[".stm"] = "text/html";
            types[".svg"] = "image/svg+xml";
            types[".sv4cpio"] = "application/x-sv4cpio";
            types[".sv4crc"] = "application/x-sv4crc";
            types[".swf"] = "application/x-shockwave-flash";
            types[".t"] = "application/x-troff";
            types[".tar"] = "application/x-tar";
            types[".tcl"] = "application/x-tcl";
            types[".tex"] = "application/x-tex";
            types[".texi"] = "application/x-texinfo";
            types[".texinfo"] = "application/x-texinfo";
            types[".tgz"] = "application/x-compressed";
            types[".tif"] = "image/tiff";
            types[".tiff"] = "image/tiff";
            types[".tr"] = "application/x-troff";
            types[".trm"] = "application/x-msterminal";
            types[".tsv"] = "text/tab-separated-values";
            types[".txt"] = "text/plain";
            types[".uls"] = "text/iuls";
            types[".ustar"] = "application/x-ustar";
            types[".vcf"] = "text/x-vcard";
            types[".vrml"] = "x-world/x-vrml";
            types[".wav"] = "audio/x-wav";
            types[".wcm"] = "application/vnd.ms-works";
            types[".wdb"] = "application/vnd.ms-works";
            types[".wks"] = "application/vnd.ms-works";
            types[".wmf"] = "application/x-msmetafile";
            types[".wps"] = "application/vnd.ms-works";
            types[".wri"] = "application/x-mswrite";
            types[".wrl"] = "x-world/x-vrml";
            types[".wrz"] = "x-world/x-vrml";
            types[".xaf"] = "x-world/x-vrml";
            types[".xbm"] = "image/x-xbitmap";
            types[".xla"] = "application/vnd.ms-excel";
            types[".xlc"] = "application/vnd.ms-excel";
            types[".xlm"] = "application/vnd.ms-excel";
            types[".xls"] = "application/vnd.ms-excel";
            types[".xlt"] = "application/vnd.ms-excel";
            types[".xlw"] = "application/vnd.ms-excel";
            types[".xof"] = "x-world/x-vrml";
            types[".xpm"] = "image/x-xpixmap";
            types[".xwd"] = "image/x-xwindowdump";
            types[".z"] = "application/x-compress";
            types[".zip"] = "application/zip";
            types[".txt"] = "text/html";
        }

        public static MimeTypes Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MimeTypes();
                    }
                }

                return instance;
            }
        }
    }
}
