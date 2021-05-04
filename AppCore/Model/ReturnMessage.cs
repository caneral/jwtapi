namespace AppCore.Model
{
    /// <summary>
    /// Dönüş Mesajı
    /// </summary>

    public class ReturnMessage
    {
        /// <summary>
        /// Başarılı Olma Durumu
        /// </summary>
        public bool isSuccessed { get; private set; }

        /// <summary>
        /// Mesaj İçeriği
        /// </summary>
        public string message { get; private set; }

        //Constructor
        public ReturnMessage()
        {
            isSuccessed = false;
            message = string.Empty;
        }
        /// <summary>
        /// Başarılı Mesajları Yazdıran Metot
        /// </summary>
        public void SetSuccessMessage(string successMessage)
        {
            isSuccessed = true;
            message = successMessage;
        }

        /// <summary>
        /// Hata Mesajları Yazdıran Metot
        /// </summary>
        public void SetErrorMessage(string errorMessage)
        {
            isSuccessed = false;
            message = errorMessage;
        }

    }
}
