namespace SafeShareAPI.Model
{
    public struct ApiResponse
    {
        public byte Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public struct ApiRequest<T>
    {
        public object AdditionalData { get; set; }
        public T Data { get; set; }
    }
    public struct AuthModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public struct UserPassword
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
    public struct UserOTP
    {
        public string TenantCode { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Otp { get; set; }
        public string CountryCode { get; set; }
    }
    public struct ActivationRequest
    {
        public int UserId { get; set; }
        public string TenantCode { get; set; }
    }
    public struct UserRegistration
    {
        public int Id { get; set; }
        public string TenantCode { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string CountryCode { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ResetToken { get; set; }
        public string WebLink { get; set; }

        public bool IsActive { get; set; }
        public bool IsSendLink { get; set; }
    }
}
