# 📦 React + TypeScript + Vite

This project is a React application built with TypeScript and Vite. It provides a minimal setup with hot module replacement (HMR) and ESLint rules for code quality.

---

## 🛠️ Getting Started

### Prerequisites
Make sure you have the following installed:
- [Node.js](https://nodejs.org/) (version 22 or higher)
- [npm](https://www.npmjs.com/) or [yarn](https://yarnpkg.com/)

### Installation
1. Clone the repository:
```bash
git clone https://github.com/Pandamonium-Development/base-reservation.git
cd site-admin
```

2. Install dependencies:
```bash
npm install
# or
yarn install
```

3. Start the development server:
```bash
npm run dev
# or
yarn dev
```

4. Make sure to configure your .env file or .env.local
replacing the variable `VITE_API_BASERESERVATION_BASE_URL` with the corresponding api url

5. Make sure api is running

6. Open your browser and navigate to:
```bash
http://localhost:5173
```

## 📁 Project structure
```bash
site-admin
├── public/                 # Static assets (e.g., images, icons)
├── src/
│   ├── assets/             # Static assets imported into the app
│   ├── components/         # Reusable React components
│   ├── pages/              # Page components for routing
│   ├── styles/             # Global and component-specific styles
│   ├── App.tsx             # Main application component
│   ├── main.tsx            # Entry point for the application
│   └── vite-env.d.ts       # TypeScript definitions for Vite
├── .eslintrc.cjs           # ESLint configuration
├── tsconfig.json           # TypeScript configuration
├── vite.config.ts          # Vite configuration
└── package.json            # Project metadata and dependencies
```

## 📚 Additional Resources

- [Next.js Documentation](https://nextjs.org/docs)
- [TypeScript Guide](https://www.typescriptlang.org/docs/)
- [Docker Guide](https://docs.docker.com/get-started/)