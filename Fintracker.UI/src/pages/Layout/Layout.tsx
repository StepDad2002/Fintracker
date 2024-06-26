﻿import {Link, Outlet} from "react-router-dom";
import {motion} from "framer-motion";
import {useEffect, useState} from "react";
// @ts-ignore
import logo from "../../../public/logo.png"
import useUserStore from "../../stores/userStore.ts";
import NavigationLink from "../../components/other/NavigationLink.tsx";
import MobileNavigationLink from "../../components/other/MobileNavigationLink.tsx";

export default function Layout() {


    return (
        <div className={'flex flex-col min-h-screen overflow-hidden bg-stone-100'}>
            <header>
                <NavBar/>
            </header>
            <main className={'flex-grow'}>
                <Outlet/>
            </main>
            <footer className={'flex-shrink-0'}>
                <Footer/>
            </footer>
        </div>
    )
}

const Footer = () => {
    return (
        <footer className="border-t-2 border-t-gray-300 p-6 mt-6">
            <div className={'flex justify-center items-center gap-x-4'}>
                <Link to="/about" className="underline hover:no-underline">
                    About us
                </Link>
                <Link to="/bank" className="underline hover:no-underline">
                    Bank Connect
                </Link>
                <a href="#" className="underline hover:no-underline"
                   onClick={(e) => {
                       e.preventDefault();
                       window.scrollTo({top: 0, behavior: 'smooth'});
                   }}>
                    Go up
                </a>
            </div>
            <p className="text-sm text-center">Цей сайт був розроблений мною у 2024 році</p>
        </footer>
    )
}
const NavBarHeader = () => (
    <Link to={'/'}>
        <div className={'flex mt-1.5 space-x-3 items-center mr-7'}>
            <img src={logo} alt="logo" className={'max-w-16 max-h-16 hidden sm:inline'}/>
            <h1 className={'text-2xl sm:text-3xl font-bold'}>Fintracker</h1>
        </div>
    </Link>
);

const NavBarButton = ({motionNav, handleButtonClick}: { motionNav: boolean, handleButtonClick: () => void }) => (
    <motion.nav className="sm:hidden container mx-auto">
        <motion.div className={'container mx-auto h-1 w-full'}>
            <motion.div className={'flex flex-col items-end'}>
                <motion.button
                    initial="hide"
                    animate={motionNav ? "show" : "hide"}
                    variants={{
                        show: {
                            y: 0
                        },
                        hide: {
                            y: 0,
                            x: 0,
                            scale: 1,
                            transition: {
                                delay: 1
                            }
                        }
                    }}
                    onClick={handleButtonClick}
                    className="flex flex-col space-y-1 relative z-10"
                >
                    <motion.span
                        variants={{
                            hide: {
                                rotate: 0
                            },
                            show: {
                                rotate: 45,
                                y: 5,
                                backgroundColor: '#e26031'
                            },
                        }}
                        className="w-6 bg-white h-1 block"
                    ></motion.span>
                    <motion.span
                        variants={{
                            hide: {
                                opacity: 1,
                                scale: 1
                            },
                            show: {
                                opacity: 0,
                                scale: 0,
                                transition: {
                                    duration: 0.1
                                }
                            },
                        }}
                        className="w-6 bg-white h-1 block"
                    ></motion.span>
                    <motion.span
                        variants={{
                            hide: {
                                rotate: 0
                            },
                            show: {
                                rotate: -45,
                                y: -11,
                                backgroundColor: '#e26031'
                            },
                        }}
                        className="w-6 bg-white h-1 block"
                    ></motion.span>
                </motion.button>
            </motion.div>
        </motion.div>
    </motion.nav>
);

interface NavLinkModel {
    to: string,
    text: string,
    show: boolean,
}

const NavBarLinks = ({motionNav, navLinks}: { motionNav: boolean, navLinks: NavLinkModel[] }) => (
    <>
        {navLinks.map((link, i) => link.show &&
            <MobileNavigationLink text={link.text} to={link.to} key={i} motionNav={motionNav}/>
        )}
        <RenderLoginLogoutLinks isMobile={true} motionNav={motionNav}/>
    </>
);

const NavBar = () => {
    const [motionNav, toggleMotionNav] = useState(false);
    const [windowWidth, setWindowWidth] = useState(window.innerWidth);

    useEffect(() => {
        const handleResize = () => {
            setWindowWidth(window.innerWidth);
            if (window.innerWidth > 640 && motionNav) {
                toggleMotionNav(false);
            }
        };

        window.addEventListener('resize', handleResize);

        return () => {
            window.removeEventListener('resize', handleResize);
        };
    }, [motionNav]);

    useEffect(() => {
        if (windowWidth > 640) {
            toggleMotionNav(false);
        }
    }, [windowWidth]);

    const navLinks = [
        {
            to: '/about',
            text: 'About us',
            show: true,
        },
        {
            to: '/bank',
            text: 'Bank',
            show: true,
        },
    ]


    return (
        <motion.header
            initial={false}
            animate={motionNav ? 'show' : 'hide'}
            variants={{
                show: {height: '7rem'},
                hide: {height: window.innerWidth > 640 ? '6rem' : '3rem'}
            }}
            className={'bg-green-400 px-2 overflow-hidden flex flex-col justify-between sm:justify-center'}
        >
            <div className={'flex items-center sm:justify-between'}>
                <NavBarHeader/>
                <NavBarButton motionNav={motionNav} handleButtonClick={() => toggleMotionNav(p => !p)}/>
                <div className={'hidden sm:flex justify-evenly sm:items-center'}>
                    {navLinks.map((link, i) =>
                        link.show && <NavigationLink to={link.to} text={link.text} key={i}/>
                    )}
                    <RenderLoginLogoutLinks isMobile={false} motionNav={null}/>
                </div>
            </div>
            <div className={'flex sm:hidden justify-around mb-4 w-full'}>
                <NavBarLinks motionNav={motionNav} navLinks={navLinks}/>
            </div>

        </motion.header>
    );
};


const RenderLoginLogoutLinks = ({isMobile, motionNav}: {
    isMobile: boolean,
    motionNav: boolean | null
}) => {
    const isUserSignedIn = useUserStore(x => x.isSignedIn());

    const logLink = isMobile ?
        <MobileNavigationLink motionNav={motionNav ? motionNav : false} isLogin={isUserSignedIn}
                              text={isUserSignedIn ? 'Log out' : 'Log in'} to={'/login'}/> :
        <NavigationLink isLogin={isUserSignedIn} text={isUserSignedIn ? 'Log out' : 'Log in'} to={'/login'}/>

    const startLink = isMobile ? <MobileNavigationLink to={'/dashboard'} text={'Start'}
                                                       motionNav={motionNav ? motionNav : false}/> :
        <NavigationLink to={'/dashboard'} text={'Start'}/>

    if (isUserSignedIn) {
        return (
            <>
                {logLink}
                {startLink}
            </>
        )
    }

    return (
        <>
            {logLink}
        </>
    )
}