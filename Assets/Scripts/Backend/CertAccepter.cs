using UnityEngine.Networking;

namespace Backend
{
    public class CertAccepter : CertificateHandler
    {

        protected override bool ValidateCertificate(byte[] certificateData)
        {
            return true;
        }
    }
}