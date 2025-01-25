import { Header } from "components/Header/Header"
import { Box, CssBaseline, Stack } from "@mui/material"

export const Page = (props: {
    children: React.ReactNode,
    header?: React.ReactNode,
}) => {
    return (
        <>
            <CssBaseline />
            <Header />
            <Stack direction={'row'}
                sx={{
                    my: { xs: '5rem', sm: '3rem', md: '5rem' },
                    mx: { xs: '1rem', sm: '3rem', md: '5rem' },
                    px: '2rem',
                    py: '1rem',
                    backgroundColor: 'background.default',
                    borderRadius: '32px',
                    xs: { width: 'calc(100vh - 80px - 2rem)' },
                    sm: { width: 'calc(100vh - 80px - 6rem)' },
                    md: { width: 'calc(100vh - 80px - 10rem)' },
                    maxWidth: '100vw',
                    minHeight: { xs: 'calc(100vh - 80px - 4rem)', md: 'calc(100vh - 80px - 4rem)' },
                    boxSizing: 'border-box',
                    overflowX: 'hidden',
                }}
            >
                <Box
                    sx={{
                        flex: 1,
                        position: 'relative',
                        overflowY: 'auto',
                        overflowX: 'hidden',
                        width: '100%',
                        boxSizing: 'border-box', // Ensures padding and border don't cause overflow
                    }}>
                    {props.header}
                    <Box
                        pt={{ xs: '21px', sm: 4 }}
                        pb='2%'
                    >
                        {props.children}
                    </Box>
                </Box>
            </Stack>
        </>
    )
}