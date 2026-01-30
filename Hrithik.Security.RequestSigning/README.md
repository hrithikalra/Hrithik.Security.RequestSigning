# Hrithik.Security.RequestSigning

Enterprise-grade **HTTP Request Signing** for .NET APIs.  
Designed for **Banking, Fintech, and secure external integrations**.

This library ensures that **only trusted clients** can send valid requests
and that **requests cannot be tampered or forged in transit**.

---

## ❓ What is Request Signing?

Request signing means the **client cryptographically signs the HTTP request**
using a shared secret.

The server:
- Rebuilds the request in a canonical form
- Re-computes the signature
- Rejects the request if the signature does not match

This prevents:
- Forged requests
- Payload tampering
- Man-in-the-middle modifications

---

## 🧱 What problem does this solve?

Without request signing:
- Anyone can send a new nonce + timestamp
- Replay protection alone cannot verify **who** sent the request

With request signing:
- Only clients with a valid secret can generate a valid request
- Any modification to method, path, query, headers, or body is detected

---

## ✅ What does this library do?

- Builds a **canonical representation** of the HTTP request
- Verifies **HMAC-SHA256 signatures**
- Validates request **freshness (timestamp)**
- Integrates cleanly with **Replay Protection**
- Designed for **external & partner APIs**

---

## 🔐 Canonical Request Format

The signature is calculated over the following fields (in order):

HTTP_METHOD
PATH
QUERY_STRING
CLIENT_ID
NONCE
TIMESTAMP
BODY_HASH


All fields are joined using a newline (`\n`).

Any change to these values will invalidate the signature.

---

## 🚀 Quick Start (Server)

### 1️⃣ Register services

```csharp
services.AddRequestSigning(options =>
{
    options.AllowedClockSkew = TimeSpan.FromMinutes(5);
});


Register a signing key provider:

services.AddSingleton<ISigningKeyProvider, InMemorySigningKeyProvider>();

2️⃣ Add middleware
app.UseRequestSigning();


⚠️ This middleware should run before ReplayProtection.

📩 Required Request Headers

Every signed request must include:

X-Client-Id    → Client identifier
X-Request-Id   → Unique nonce (UUID recommended)
X-Timestamp    → Unix timestamp (UTC, seconds)
X-Signature    → Base64 HMAC-SHA256 signature

🧪 Example Client Signing (Concept)
signature = Base64(
  HMACSHA256(secret, canonicalRequest)
)


The server recomputes this signature and compares it in constant time.

🔐 Security Notes (IMPORTANT)

This library authenticates requests, not users

It does not replace OAuth, JWT, or mTLS

It is designed to work together with:

Authentication (JWT / OAuth / mTLS)

Replay protection

Optional JWE / JWS message security

Recommended middleware order
app.UseRequestSigning();
app.UseReplayProtection();

🧱 Production Usage

For production systems:

Store client secrets securely (DB / Vault / Key Management Service)

Rotate secrets periodically

Use Replay Protection to prevent duplicate execution

🔗 Related Packages

Hrithik.Security.ReplayProtection
Prevents duplicate or replayed requests

MainLibProj_Hrithik
JWE / JWS message-level encryption and signing

Together, these provide banking-grade API security.