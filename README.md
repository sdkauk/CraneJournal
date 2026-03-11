# Crane

A simple journaling app.

<!-- [Get it on Google Play](https://play.google.com/store/apps/details?id=com.cranejournal.app) -->

## Architecture

```
├── mobile/             React Native (Expo SDK 54, TypeScript)
├── CraneJournal/       ASP.NET Core 8 Web API
├── BusinessLogic/      Service layer
├── DataAccess/         MongoDB repositories
└── Common/             Shared models
```

### Mobile

- React Native 0.81 with Expo Router (file-based navigation)
- Auth0 for authentication
- Reanimated + Gesture Handler for the previous entries drawer and animations
- Small design system using ink-opacity color tokens, a serif/sans type scale, and a spacing scale

### API

- ASP.NET Core 8 with JWT Bearer auth (Auth0)
- MongoDB via the official C# driver
- `Controller → Service → Repository` pattern with interface-based DI
- Dev data seeding

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Mobile | React Native, Expo, TypeScript |
| Navigation | Expo Router |
| Auth | Auth0 (react-native-auth0) |
| Animations | Reanimated, Gesture Handler |
| API | ASP.NET Core 8, C# |
| Database | MongoDB |
| Hosting | Azure App Service |
| CI/CD | GitHub Actions → Azure, EAS Build |

## Running Locally

### API

```bash
cd CraneJournal
dotnet user-secrets set "ConnectionStrings:MongoDB" "<your-connection-string>"
dotnet run
```

### Mobile

```bash
cd mobile
npm install
npx expo start
```

Create a `.env` file in `mobile/`:

```
EXPO_PUBLIC_AUTH0_DOMAIN=<your-auth0-domain>
EXPO_PUBLIC_AUTH0_CLIENT_ID=<your-auth0-client-id>
EXPO_PUBLIC_AUTH0_AUDIENCE=<your-auth0-audience>
EXPO_PUBLIC_API_BASE_URL=http://localhost:5085
