
<script lang="ts">
  import { page } from "$app/state";

  const mainNavigation = [
    { label: "Dashboard", href: "/", icon: "⌂" },
    { label: "Live Coaching", href: "/live", icon: "◉" },
    { label: "Sessions", href: "/sessions", icon: "▤" },
    { label: "Performance Analysis", href: "/analysis", icon: "⌁" }
  ];

  const applicationNavigation = [
    { label: "Settings", href: "/settings", icon: "⚙" }
  ];

  function isActive(href: string): boolean {
    const currentPath = page.url.pathname;

    if (href === "/") {
      return currentPath === "/";
    }

    return currentPath === href || currentPath.startsWith(`${href}/`);
  }
</script>

<aside class="box-border flex min-h-screen w-[240px] min-w-[240px] flex-col border-r border-[#303641] bg-[#181b21] px-4 pt-6 pb-4">

  <!-- Branding -->
  <div class="flex items-center gap-3 px-2 pb-8">
    <div class="flex h-10 w-10 shrink-0 items-center justify-center rounded-[10px] bg-[#00c2ff] text-[15px] font-extrabold text-[#101216]">
      AI
    </div>

    <div>
      <h1 class="m-0 text-[15px] font-bold text-[#f3f4f6]">
        AI Racing Coach
      </h1>

      <span class="mt-1 block text-[9px] tracking-[0.7px] text-[#929aa8]">
        DRIVER PERFORMANCE SYSTEM
      </span>
    </div>
  </div>

  <!-- Navigation -->
  <div class="flex flex-col gap-3">

    <span class="px-[10px] text-[10px] font-bold tracking-[1.2px] text-[#737d8c]">
      MAIN
    </span>

    <nav aria-label="Main navigation" class="flex flex-col gap-[5px]">
      {#each mainNavigation as item}
        <a
          href={item.href}
          class={`flex min-h-[42px] items-center gap-3 rounded-[7px] px-3 text-[13px] no-underline transition-colors duration-150 ${
            isActive(item.href)
              ? "bg-[#123343] font-semibold text-[#00c2ff]"
              : "text-[#a5adba] hover:bg-[#252a33] hover:text-[#f3f4f6]"
          }`}
          aria-current={isActive(item.href) ? "page" : undefined}
        >
          <span class="w-5 text-center text-lg">
            {item.icon}
          </span>

          <span>{item.label}</span>
        </a>
      {/each}
    </nav>

    <span class="mt-6 px-[10px] text-[10px] font-bold tracking-[1.2px] text-[#737d8c]">
      APPLICATION
    </span>

    <nav aria-label="Application navigation" class="flex flex-col gap-[5px]">
      {#each applicationNavigation as item}
        <a
          href={item.href}
          class={`flex min-h-[42px] items-center gap-3 rounded-[7px] px-3 text-[13px] no-underline transition-colors duration-150 ${
            isActive(item.href)
              ? "bg-[#123343] font-semibold text-[#00c2ff]"
              : "text-[#a5adba] hover:bg-[#252a33] hover:text-[#f3f4f6]"
          }`}
          aria-current={isActive(item.href) ? "page" : undefined}
        >
          <span class="w-5 text-center text-lg">
            {item.icon}
          </span>

          <span>{item.label}</span>
        </a>
      {/each}
    </nav>

  </div>

  <!-- Footer -->
  <div class="mt-auto pt-5">

    <!-- Backend status -->
    <div class="flex items-center gap-[10px] px-2 py-3">
      <span class="h-2 w-2 shrink-0 rounded-full bg-amber-400"></span>

      <div>
        <span class="block text-xs text-[#d1d5db]">
          Backend API
        </span>

        <span class="mt-[3px] block text-[10px] text-[#737d8c]">
          Status on Dashboard
        </span>
      </div>
    </div>

    <!-- Simulator status -->
    <div class="flex items-center gap-[10px] px-2 py-3">
      <span class="h-2 w-2 shrink-0 rounded-full bg-red-500"></span>

      <div>
        <span class="block text-xs text-[#d1d5db]">
          Assetto Corsa
        </span>

        <span class="mt-[3px] block text-[10px] text-[#737d8c]">
          Not connected
        </span>
      </div>
    </div>

    <!-- Version -->
    <div class="mt-4 flex justify-between border-t border-[#303641] px-2 pt-[14px] text-[10px] text-[#737d8c]">
      <span>AI Racing Coach</span>
      <span>v0.1.0</span>
    </div>

  </div>
</aside>
