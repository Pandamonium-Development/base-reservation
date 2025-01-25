import { theme } from './theme.ts';
import { ThemeProvider } from '@mui/material'
import { HelmetProvider } from 'react-helmet-async';
import { Navigation } from './navigation/Navigation.tsx'
import { HeadLinks } from 'components/Head/HeadLinks.tsx';
import { Snackbar } from 'components/Shared/Snackbar.tsx';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

const queryClient = new QueryClient();

export function App() {
  return (
    <HelmetProvider>
      <HeadLinks />
      <ThemeProvider theme={theme}>
        <QueryClientProvider client={queryClient}>
          <Snackbar />
          <Navigation />
        </QueryClientProvider>
      </ThemeProvider>
    </HelmetProvider>
  )
}
