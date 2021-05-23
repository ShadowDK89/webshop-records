import React from 'react'

export default function logo() {
        return (
                <svg width="70" height="70" viewBox="0 0 70 70" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <circle cx="35" cy="35" r="35" fill="#262626"/>
                    <circle cx="35" cy="35" r="29.5" fill="#262626" stroke="url(#paint0_linear)"/>
                    <circle cx="34.5" cy="34.5" r="22" fill="#262626" stroke="url(#paint1_linear)"/>
                    <circle cx="34.5" cy="34.5" r="11.5" fill="#EFEFEF"/>
                    <circle cx="35" cy="35" r="4" fill="#171717"/>
                    <defs>
                        <linearGradient id="paint0_linear" x1="74" y1="10" x2="18" y2="37" gradientUnits="userSpaceOnUse">
                            <stop stop-color="white"/>
                            <stop offset="1" stop-color="#D8D8D8" stop-opacity="0"/>
                        </linearGradient>
                        <linearGradient id="paint1_linear" x1="75.5" y1="12" x2="26.5" y2="40.5" gradientUnits="userSpaceOnUse">
                            <stop stop-color="white"/>
                            <stop offset="1" stop-color="#E0E0E0" stop-opacity="0"/>
                        </linearGradient>
                    </defs>
                </svg>
        );
}
