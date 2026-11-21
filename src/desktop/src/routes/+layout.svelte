<script lang="ts">
  import Sidebar from "$lib/components/layout/Sidebar.svelte";
  import "../app.css";

  let { children } = $props();
</script>

<div class="flex min-h-screen bg-[#101216] text-[#f3f4f6]">
  <Sidebar />

  <main class="min-w-0 flex-1 overflow-x-hidden">
    {@render children()}
  </main>
</div>
