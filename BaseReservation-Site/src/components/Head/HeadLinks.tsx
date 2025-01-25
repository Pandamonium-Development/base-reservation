import { Helmet } from 'react-helmet-async';

export const HeadLinks = () => {
    return (
        <Helmet>
            <link
                href="https://fonts.cdnfonts.com/css/satoshi"
                rel="stylesheet"
            />
            <link
                href="https://fonts.cdnfonts.com/css/angel-rhapsody"
                rel="stylesheet"
            />
        </Helmet>
    );
};
