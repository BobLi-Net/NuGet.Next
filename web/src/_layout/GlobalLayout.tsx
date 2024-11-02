import { useUserStore } from '@/store/user';
import { ThemeProvider, } from '@lobehub/ui'

const GlobalLayout = ({ children }: any) => {
    const [theme, setTheme] = useUserStore((s) => [s.theme, s.setTheme]);
    return (
        <ThemeProvider
            themeMode={theme}
            onThemeModeChange={(mode) => {
                setTheme(mode);
            }}>
            {children}
        </ThemeProvider>
    );
};

export default GlobalLayout;