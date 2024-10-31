import { ThemeProvider,  } from '@lobehub/ui'

const GlobalLayout = ({ children }: any) => {
    return (
        <ThemeProvider>
            {children}
        </ThemeProvider>
    );
};

export default GlobalLayout;