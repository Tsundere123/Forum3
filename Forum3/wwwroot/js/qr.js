// From: https://learn.microsoft.com/en-gb/aspnet/core/security/authentication/identity-enable-qrcodes?view=aspnetcore-6.0
window.addEventListener("load", () => {
    const uri = document.getElementById("qrCodeData").getAttribute('data-url');
    new QRCode(document.getElementById("qrCode"),
        {
            text: uri,
            width: 150,
            height: 150
        });
});