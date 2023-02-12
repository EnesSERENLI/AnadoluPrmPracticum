using AnadoluPrmPracticum.Utils.Abstract;
using System.Text;

namespace AnadoluPrmPracticum.Utils.Concrete
{
    public class FileLogger :ILoggerService
    {
        private StringBuilder _sb;
        public StringBuilder sb
        {
            get
            {
                if (_sb == null)
                {
                    _sb = new StringBuilder();
                }
                return _sb;
            }
        }
        public void Log(string message)
        {
            try
            {
                sb.Append(message);
                sb.AppendLine();
                File.WriteAllText("C:\\Users\\ENES PC\\Desktop\\AnadoluPrmPracticum_RequestLogger.txt", sb.ToString()); //kendi masaüstümde bir txt dosyası oluşturup oraya kaydediyorum.. Hangi metota istek gelmiş cevap ne dönmüş veya hata varsa neyden dolayı kaynaklanmış hepsi işleniyor..
            }
            catch (Exception ex)
            {
                sb.Append("Error");
                sb.AppendLine();
                sb.Append(ex.Message);
                File.WriteAllText("C:\\Users\\ENES PC\\Desktop\\AnadoluPrmPracticum_RequestLogger.txt", sb.ToString());
            }
        }
    }
}
