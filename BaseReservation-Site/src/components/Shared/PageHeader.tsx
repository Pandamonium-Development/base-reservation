import { ArrowLeft } from "@mui/icons-material"
import { Box, Typography } from "@mui/material"
import { useLayout } from 'hooks/useLayout'
import { isNil } from "lodash"
import { Link } from "react-router-dom"

interface PageHeaderProps {
    children?: React.ReactNode
    title: string
    subtitle?: string
    backText?: string
    backPath?: string
    actionButton?: React.ReactNode
}

export const PageHeader = ({
    children,
    title,
    subtitle,
    backText,
    backPath,
    actionButton
}: PageHeaderProps) => {
    const { isMobile } = useLayout()
    return (
        <Box mt={4}>
            <Box>
                {!isNil(backPath) && (
                    <Link to={backPath} style={{ textDecoration: 'none' }}>
                        <Box
                            display='flex'
                            flexDirection='row'
                            alignItems='center'
                            gap='1'
                            mb={4}
                            sx={{
                                color: 'white',
                                '&:hover': {
                                    color: 'text.secondary',
                                },
                            }}
                        >
                            <ArrowLeft sx={{ color: 'inherit' }} />
                            <Typography sx={{ color: 'inherit' }}>Regresar a {backText}</Typography>
                        </Box>
                    </Link>
                )}

                <Box
                    sx={{
                        display: 'flex',
                        width: '100%',
                        flexDirection: isMobile ? 'column' : 'row',
                        justifyContent: 'space-between',
                        alignItems: isMobile ? 'flex-start' : 'self-end',
                    }}
                >
                    <Box
                        sx={{
                            width: isMobile ? '100%' : 'auto',
                            textAlign: isMobile ? 'center' : 'left',
                        }}
                    >
                        <Typography variant="h2" >{title}</Typography>
                        <Box>
                            {!isNil(subtitle) && (
                                <Typography variant="subtitle2" >{subtitle}</Typography>
                            )}
                        </Box>
                        <Box>{children}</Box>
                    </Box>
                    {actionButton &&
                        <Box sx={{
                            width: isMobile ? '100%' : 'auto',
                            textAlign: isMobile ? 'center' : 'right',
                        }}
                        >
                            {actionButton}
                        </Box>
                    }
                </Box>
            </Box>
        </Box>
    )
}