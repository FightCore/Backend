using Newtonsoft.Json;

namespace FightCore.Backend.Configuration.Authentication
{
	public class FirebaseAuthenticationClaim
	{
		[JsonProperty("sign_in_provider")]
		public string SignInProvider { get; set; }
	}
}
